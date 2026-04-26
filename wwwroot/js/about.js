(() => {
    const prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    const menuBtn = document.getElementById("menuBtn");
    const mobileDrawer = document.getElementById("mobileDrawer");
    const fadeUpElements = document.querySelectorAll(".fade-up");
    const statNums = document.querySelectorAll(".stat-num[data-target]");

    if (menuBtn && mobileDrawer) {
        menuBtn.addEventListener("click", () => {
            const isOpen = mobileDrawer.classList.toggle("open");
            menuBtn.setAttribute("aria-expanded", String(isOpen));
        });
    }

    if (fadeUpElements.length > 0) {
        if (prefersReducedMotion) {
            fadeUpElements.forEach((el) => el.classList.add("visible"));
        } else {
            const fadeObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (!entry.isIntersecting) return;
                    entry.target.classList.add("visible");
                    observer.unobserve(entry.target);
                });
            }, { threshold: 0.12 });

            fadeUpElements.forEach((el) => fadeObserver.observe(el));
        }
    }

    const animateCount = (el) => {
        if (el.dataset.done === "true") return;
        const target = Number(el.dataset.target || 0);
        const suffix = el.dataset.suffix || "";
        let value = 0;
        const step = Math.ceil(target / 60);
        const timer = window.setInterval(() => {
            value += step;
            if (value >= target) {
                value = target;
                el.textContent = `${value.toLocaleString()}${suffix}`;
                el.dataset.done = "true";
                window.clearInterval(timer);
                return;
            }
            el.textContent = `${value.toLocaleString()}${suffix}`;
        }, 22);
    };

    if (statNums.length > 0) {
        if (prefersReducedMotion) {
            statNums.forEach((el) => {
                const target = Number(el.dataset.target || 0);
                const suffix = el.dataset.suffix || "";
                el.textContent = `${target.toLocaleString()}${suffix}`;
                el.dataset.done = "true";
            });
        } else {
            const countObserver = new IntersectionObserver((entries) => {
                entries.forEach((entry) => {
                    if (!entry.isIntersecting) return;
                    animateCount(entry.target);
                });
            }, { threshold: 0.5 });

            statNums.forEach((el) => countObserver.observe(el));
        }
    }
})();
