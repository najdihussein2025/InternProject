(() => {
    const prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;
    const nav = document.getElementById("site-nav");
    const menuToggle = document.getElementById("menu-toggle");
    const drawer = document.getElementById("mobile-drawer");
    const counterElements = document.querySelectorAll("[data-counter]");
    const revealElements = document.querySelectorAll(".reveal");
    const statsSection = document.getElementById("stats");

    const setNavState = () => {
        if (!nav) return;
        nav.classList.toggle("scrolled", window.scrollY > 60);
    };

    const closeDrawer = () => {
        if (!nav || !menuToggle) return;
        nav.classList.remove("open");
        menuToggle.setAttribute("aria-expanded", "false");
    };

    const openDrawer = () => {
        if (!nav || !menuToggle) return;
        nav.classList.add("open");
        menuToggle.setAttribute("aria-expanded", "true");
    };

    setNavState();
    window.addEventListener("scroll", setNavState, { passive: true });

    if (menuToggle && drawer) {
        menuToggle.addEventListener("click", () => {
            const isOpen = nav?.classList.contains("open");
            if (isOpen) closeDrawer();
            else openDrawer();
        });

        drawer.querySelectorAll("a").forEach((link) => {
            link.addEventListener("click", closeDrawer);
        });

        window.addEventListener("resize", () => {
            if (window.innerWidth > 768) closeDrawer();
        });
    }

    document.querySelectorAll('a[href^="#"]').forEach((anchor) => {
        anchor.addEventListener("click", (event) => {
            const targetId = anchor.getAttribute("href");
            if (!targetId || targetId === "#") return;
            const target = document.querySelector(targetId);
            if (!target) return;

            event.preventDefault();
            if (prefersReducedMotion) target.scrollIntoView();
            else target.scrollIntoView({ behavior: "smooth", block: "start" });
        });
    });

    const animateCounter = (element) => {
        const target = Number(element.getAttribute("data-target") || 0);
        const suffix = element.getAttribute("data-suffix") || "";
        const duration = 1200;
        const start = performance.now();

        const update = (now) => {
            const progress = Math.min((now - start) / duration, 1);
            const eased = 1 - Math.pow(1 - progress, 3);
            const value = Math.floor(target * eased);
            element.textContent = `${value.toLocaleString()}${suffix}`;

            if (progress < 1) {
                window.requestAnimationFrame(update);
            }
        };

        window.requestAnimationFrame(update);
    };

    if (statsSection && counterElements.length > 0) {
        if (prefersReducedMotion) {
            counterElements.forEach((counter) => {
                const target = Number(counter.getAttribute("data-target") || 0);
                const suffix = counter.getAttribute("data-suffix") || "";
                counter.textContent = `${target.toLocaleString()}${suffix}`;
            });
        } else {
            const counterObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (!entry.isIntersecting) return;
                    counterElements.forEach(animateCounter);
                    observer.unobserve(entry.target);
                });
            }, { threshold: 0.35 });

            counterObserver.observe(statsSection);
        }
    }

    if (revealElements.length > 0) {
        if (prefersReducedMotion) {
            revealElements.forEach((el) => el.classList.add("show"));
        } else {
            const revealObserver = new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (!entry.isIntersecting) return;
                    entry.target.classList.add("show");
                    observer.unobserve(entry.target);
                });
            }, { threshold: 0.2, rootMargin: "0px 0px -20px 0px" });

            revealElements.forEach((el) => revealObserver.observe(el));
        }
    }

    document.querySelectorAll(".ripple-btn").forEach((button) => {
        button.addEventListener("click", (event) => {
            if (prefersReducedMotion) return;
            const rect = button.getBoundingClientRect();
            const ripple = document.createElement("span");
            ripple.className = "ripple";
            ripple.style.left = `${event.clientX - rect.left}px`;
            ripple.style.top = `${event.clientY - rect.top}px`;
            button.appendChild(ripple);
            ripple.addEventListener("animationend", () => ripple.remove());
        });
    });
})();
