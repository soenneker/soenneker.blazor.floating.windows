export class FloatingWindowInterop {
    constructor() {
        this.windows = new Map();
        this.activeWindow = null;
        this.nextZIndex = 1000;
    }

    async create(id, optionsJson) {
        const options = JSON.parse(optionsJson);

        const window = document.getElementById(id);
        
        // Check if element exists before proceeding
        if (!window) {
            console.error('FloatingWindowInterop.create: Element with id', id, 'not found');
            return;
        }

        if (!options?.enabled) {
            if (window) {
                window.style.display = 'none';
            }
            return;
        }

        // Set initial styles
        try {
            window.style.position = 'fixed';
            window.style.zIndex = options.zIndex || this.nextZIndex++;

            if (options.autoSizeToContent) {
                window.style.width = 'auto';
                window.style.height = 'auto';
                window.style.maxWidth = '';
                window.style.maxHeight = '';
                window.style.minWidth = '';
                window.style.minHeight = '';
            } else {
                if (options.width) {
                    window.style.width = `${options.width}px`;
                }
                if (options.height) {
                    window.style.height = `${options.height}px`;
                }
            }
            if (options.initialX !== undefined) {
                window.style.left = `${options.initialX}px`;
            }
            if (options.initialY !== undefined) {
                window.style.top = `${options.initialY}px`;
            }
        } catch (error) {
            console.error('Error setting initial styles for window', id, ':', error);
        }

        // --- Dynamic auto-size to content ---
        let resizeObserver = null;
        if (options.dynamicAutoSizeToContent) {
            const content = window.querySelector('.floating-window-content');
            if (content) {
                resizeObserver = new ResizeObserver(() => {
                    window.style.width = 'auto';
                    window.style.height = 'auto';
                    // Notify .NET to recenter if enabled
                    const windowData = this.windows.get(id);
                    if (options.recenterOnResize && windowData && windowData.dotNetRef) {
                        windowData.dotNetRef.invokeMethodAsync('OnContentResized').catch(console.error);
                    }
                });
                resizeObserver.observe(content);
            }
        }

        // Store window data
        this.windows.set(id, {
            element: window,
            options: options,
            isDragging: false,
            isResizing: false,
            dragStart: { x: 0, y: 0 },
            resizeStart: { width: 0, height: 0, x: 0, y: 0 },
            resizeDirection: null,
            resizeObserver: resizeObserver
        });

        // Setup dragging
        if (options.draggable) {
            this.setupDragging(id);
        }

        // Setup resizing
        if (options.resizable) {
            this.setupResizing(id);
        }
    }

    setupDragging(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        const titleBar = windowData.element.querySelector('.floating-window-titlebar');
        if (!titleBar) return;

        const onMouseDown = (e) => {
            e.preventDefault();
            this.startDragging(id, e);
        };

        titleBar.addEventListener('mousedown', onMouseDown);
        titleBar.style.cursor = 'move';

        // Store cleanup function
        windowData.cleanupDragging = () => {
            titleBar.removeEventListener('mousedown', onMouseDown);
        };
    }

    setupResizing(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        const resizeHandles = windowData.element.querySelectorAll('.floating-window-resize-handle');
        
        resizeHandles.forEach(handle => {
            const direction = handle.dataset.direction;
            const onMouseDown = (e) => {
                e.preventDefault();
                this.startResizing(id, e, direction);
            };

            handle.addEventListener('mousedown', onMouseDown);
            
            // Store cleanup function
            if (!windowData.cleanupResizing) {
                windowData.cleanupResizing = [];
            }
            windowData.cleanupResizing.push(() => {
                handle.removeEventListener('mousedown', onMouseDown);
            });
        });
    }

    startDragging(id, e) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        windowData.isDragging = true;
        windowData.dragStart = {
            x: e.clientX - windowData.element.offsetLeft,
            y: e.clientY - windowData.element.offsetTop
        };

        this.bringToFront(id);
        this.activeWindow = id;

        const onMouseMove = (e) => {
            if (!windowData.isDragging) return;
            this.updateDragPosition(id, e);
        };

        const onMouseUp = () => {
            windowData.isDragging = false;
            this.activeWindow = null;
            document.removeEventListener('mousemove', onMouseMove);
            document.removeEventListener('mouseup', onMouseUp);
        };

        document.addEventListener('mousemove', onMouseMove);
        document.addEventListener('mouseup', onMouseUp);
    }

    startResizing(id, e, direction) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        windowData.isResizing = true;
        windowData.resizeDirection = direction;
        windowData.resizeStart = {
            width: windowData.element.offsetWidth,
            height: windowData.element.offsetHeight,
            x: parseInt(windowData.element.style.left) || 0,
            y: parseInt(windowData.element.style.top) || 0,
            mouseX: e.clientX,
            mouseY: e.clientY
        };

        this.bringToFront(id);
        this.activeWindow = id;

        const onMouseMove = (e) => {
            if (!windowData.isResizing) return;
            this.updateResizePosition(id, e);
        };

        const onMouseUp = () => {
            windowData.isResizing = false;
            windowData.resizeDirection = null;
            this.activeWindow = null;
            document.removeEventListener('mousemove', onMouseMove);
            document.removeEventListener('mouseup', onMouseUp);
        };

        document.addEventListener('mousemove', onMouseMove);
        document.addEventListener('mouseup', onMouseUp);
    }

    updateDragPosition(id, e) {
        const windowData = this.windows.get(id);
        if (!windowData || !windowData.isDragging) return;

        let newX = e.clientX - windowData.dragStart.x;
        let newY = e.clientY - windowData.dragStart.y;

        // Constrain to viewport if enabled
        if (windowData.options.constrainToViewport) {
            const rect = windowData.element.getBoundingClientRect();
            const viewportWidth = window.innerWidth;
            const viewportHeight = window.innerHeight;

            newX = Math.max(0, Math.min(newX, viewportWidth - rect.width));
            newY = Math.max(0, Math.min(newY, viewportHeight - rect.height));
        }

        windowData.element.style.left = `${newX}px`;
        windowData.element.style.top = `${newY}px`;
    }

    updateResizePosition(id, e) {
        const windowData = this.windows.get(id);
        if (!windowData || !windowData.isResizing) return;

        const deltaX = e.clientX - windowData.resizeStart.mouseX;
        const deltaY = e.clientY - windowData.resizeStart.mouseY;
        const direction = windowData.resizeDirection;
        let newWidth = windowData.resizeStart.width;
        let newHeight = windowData.resizeStart.height;
        let newX = windowData.resizeStart.x;
        let newY = windowData.resizeStart.y;
        if (direction.includes('e')) {
            newWidth = Math.max(windowData.options.minWidth, windowData.resizeStart.width + deltaX);
            if (windowData.options.maxWidth) {
                newWidth = Math.min(windowData.options.maxWidth, newWidth);
            }
        }
        if (direction.includes('w')) {
            const maxWidthChange = windowData.resizeStart.width - windowData.options.minWidth;
            const clampedDeltaX = Math.max(-maxWidthChange, Math.min(deltaX, maxWidthChange));
            newWidth = windowData.resizeStart.width - clampedDeltaX;
            const originalX = windowData.resizeStart.x;
            const originalWidth = windowData.resizeStart.width;
            newX = originalX + (originalWidth - newWidth);
        }
        if (direction.includes('s')) {
            newHeight = Math.max(windowData.options.minHeight, windowData.resizeStart.height + deltaY);
            if (windowData.options.maxHeight) {
                newHeight = Math.min(windowData.options.maxHeight, newHeight);
            }
        }
        if (direction.includes('n')) {
            const maxHeightChange = windowData.resizeStart.height - windowData.options.minHeight;
            const clampedDeltaY = Math.max(-maxHeightChange, Math.min(deltaY, maxHeightChange));
            newHeight = windowData.resizeStart.height - clampedDeltaY;
            const originalY = windowData.resizeStart.y;
            const originalHeight = windowData.resizeStart.height;
            newY = originalY + (originalHeight - newHeight);
        }

        // Constrain to viewport if enabled
        if (windowData.options.constrainToViewport) {
            const viewportWidth = window.innerWidth;
            const viewportHeight = window.innerHeight;
            const minWidth = windowData.options.minWidth || 200;
            const minHeight = windowData.options.minHeight || 150;

            // Constrain width to viewport
            if (newX + newWidth > viewportWidth) {
                newWidth = Math.max(minWidth, viewportWidth - newX);
            }
            // Constrain height to viewport
            if (newY + newHeight > viewportHeight) {
                newHeight = Math.max(minHeight, viewportHeight - newY);
            }
            // Constrain X position to viewport
            if (newX < 0) {
                newX = 0;
                if (newWidth > viewportWidth) {
                    newWidth = viewportWidth;
                }
            }
            // Constrain Y position to viewport
            if (newY < 0) {
                newY = 0;
                if (newHeight > viewportHeight) {
                    newHeight = viewportHeight;
                }
            }
            newWidth = Math.max(minWidth, newWidth);
            newHeight = Math.max(minHeight, newHeight);
        }

        windowData.element.style.width = `${newWidth}px`;
        windowData.element.style.height = `${newHeight}px`;
        windowData.element.style.left = `${newX}px`;
        windowData.element.style.top = `${newY}px`;
    }

    setCallbacks(id, dotNetRef) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        windowData.dotNetRef = dotNetRef;
    }

    show(id) {
        const windowData = this.windows.get(id);
        if (!windowData) {
            console.warn('Window data not found for id:', id);
            return;
        }
        const el = windowData.element;
        if (windowData.options.centerOnShow) {
            // Phase 1: Render off-screen but hidden
            el.style.display = 'block';
            el.style.visibility = 'hidden';
            el.style.left = '-9999px';
            el.style.top = '-9999px';
            // Phase 2: After layout, measure and center
            requestAnimationFrame(() => {
                const width = el.offsetWidth;
                const height = el.offsetHeight;
                const viewportWidth = window.innerWidth;
                const viewportHeight = window.innerHeight;
                const x = Math.max(0, Math.round((viewportWidth - width) / 2));
                const y = Math.max(0, Math.round((viewportHeight - height) / 2));
                el.style.left = `${x}px`;
                el.style.top = `${y}px`;
                el.style.visibility = 'visible';
                el.classList.add('visible');
                if (windowData.dotNetRef) {
                    windowData.dotNetRef.invokeMethodAsync("InvokeOnShow").catch(console.error);
                }
            });
        } else {
            el.style.display = 'block';
            el.style.visibility = 'visible';
            el.classList.add('visible');
            if (windowData.dotNetRef) {
                windowData.dotNetRef.invokeMethodAsync("InvokeOnShow").catch(console.error);
            }
        }
    }

    hide(id) {
        const windowData = this.windows.get(id);
        if (!windowData) {
            console.warn('Window data not found for hide id:', id);
            return;
        }

        windowData.element.style.display = 'none';
        windowData.element.classList.remove('visible');
        if (windowData.dotNetRef) {
            windowData.dotNetRef.invokeMethodAsync("InvokeOnHide").catch(console.error);
        }
    }

    toggle(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        if (windowData.element.style.display === 'none') {
            this.show(id);
        } else {
            this.hide(id);
        }
    }

    close(id) {
        this.hide(id);
    }

    destroy(id) {
        const windowData = this.windows.get(id);
        if (!windowData) {
            console.warn('Window data not found for destroy id:', id);
            return;
        }

        // Cleanup event listeners
        if (windowData.cleanupDragging) {
            windowData.cleanupDragging();
        }
        if (windowData.cleanupResizing) {
            windowData.cleanupResizing.forEach(cleanup => cleanup());
        }
        // Cleanup ResizeObserver
        if (windowData.resizeObserver) {
            const content = windowData.element.querySelector('.floating-window-content');
            if (content) {
                windowData.resizeObserver.unobserve(content);
            }
            windowData.resizeObserver.disconnect();
        }

        // Remove from DOM
        if (windowData.element.parentNode) {
            windowData.element.parentNode.removeChild(windowData.element);
        }

        this.windows.delete(id);
    }

    getPosition(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return { x: 0, y: 0 };

        return {
            x: parseInt(windowData.element.style.left) || 0,
            y: parseInt(windowData.element.style.top) || 0
        };
    }

    setPosition(id, x, y) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        windowData.element.style.left = `${x}px`;
        windowData.element.style.top = `${y}px`;
    }

    getSize(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return { width: 0, height: 0 };

        return {
            width: windowData.element.offsetWidth,
            height: windowData.element.offsetHeight
        };
    }

    setSize(id, width, height) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        windowData.element.style.width = `${width}px`;
        windowData.element.style.height = `${height}px`;
    }

    bringToFront(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return;

        windowData.element.style.zIndex = this.nextZIndex++;
    }

    getViewportSize() {
        return {
            width: window.innerWidth,
            height: window.innerHeight
        };
    }

    centerInViewport(id) {
        const windowData = this.windows.get(id);
        if (!windowData) return;
        const el = windowData.element;
        // Temporarily make visible to measure if needed
        const prevDisplay = el.style.display;
        const prevVisibility = el.style.visibility;
        el.style.visibility = 'hidden';
        el.style.display = 'block';
        // Measure
        const width = el.offsetWidth;
        const height = el.offsetHeight;
        const viewportWidth = window.innerWidth;
        const viewportHeight = window.innerHeight;
        const x = Math.max(0, Math.round((viewportWidth - width) / 2));
        const y = Math.max(0, Math.round((viewportHeight - height) / 2));
        el.style.left = `${x}px`;
        el.style.top = `${y}px`;
        // Restore previous display/visibility
        el.style.display = prevDisplay;
        el.style.visibility = prevVisibility;
    }
}

window.FloatingWindowInterop = new FloatingWindowInterop();