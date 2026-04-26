(() => {
    const drawer = document.getElementById("mobileDrawer");
    const menuBtn = document.getElementById("menuBtn");
    const topicPills = document.querySelectorAll(".topic-pill");
    const messageInput = document.getElementById("message");
    const charCount = document.getElementById("charCount");
    const form = document.getElementById("contactForm");
    const formContent = document.getElementById("formContent");
    const successMsg = document.getElementById("successMsg");
    const fadeUpElements = document.querySelectorAll(".fade-up");
    const prefersReducedMotion = window.matchMedia("(prefers-reduced-motion: reduce)").matches;

    if (menuBtn && drawer) {
        menuBtn.addEventListener("click", () => {
            const isOpen = drawer.classList.toggle("open");
            menuBtn.setAttribute("aria-expanded", String(isOpen));
        });
    }

    topicPills.forEach((pill) => {
        pill.addEventListener("click", () => {
            topicPills.forEach((item) => item.classList.remove("selected"));
            pill.classList.add("selected");
        });
    });

    if (messageInput && charCount) {
        messageInput.addEventListener("input", () => {
            const value = messageInput.value.slice(0, 500);
            if (messageInput.value !== value) {
                messageInput.value = value;
            }
            charCount.textContent = String(value.length);
        });
    }

    if (form && formContent && successMsg) {
        form.addEventListener("submit", (event) => {
            event.preventDefault();
            formContent.hidden = true;
            successMsg.hidden = false;
        });
    }

    if (fadeUpElements.length > 0) {
        if (prefersReducedMotion) {
            fadeUpElements.forEach((el) => el.classList.add("visible"));
            return;
        }

        const observer = new IntersectionObserver((entries, obs) => {
            entries.forEach((entry) => {
                if (!entry.isIntersecting) return;
                entry.target.classList.add("visible");
                obs.unobserve(entry.target);
            });
        }, { threshold: 0.18 });

        fadeUpElements.forEach((el) => observer.observe(el));
    }
})();
