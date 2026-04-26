(() => {
    const eyeClosedClass = "closed";
    const eyeOpenClass = "open";

    const initPasswordToggles = () => {
        document.querySelectorAll(".toggle-password").forEach((button) => {
            const wrapper = button.closest(".input-with-icon");
            const input = wrapper?.querySelector(".password-input, .confirm-password-input");
            const eye = button.querySelector("[data-eye]");
            if (!input || !eye) return;

            eye.classList.add(eyeClosedClass);
            button.addEventListener("click", () => {
                const isHidden = input.type === "password";
                input.type = isHidden ? "text" : "password";
                eye.classList.toggle(eyeOpenClass, isHidden);
                eye.classList.toggle(eyeClosedClass, !isHidden);
            });
        });
    };

    const initStrengthMeter = () => {
        const password = document.querySelector("#Password.password-input");
        const bars = document.querySelectorAll(".strength-bars span");
        const label = document.querySelector(".strength-label");
        if (!password || bars.length !== 4 || !label) return;

        const colors = ["#DC2626", "#D97706", "#0284C7", "#059669"];
        const labels = ["Weak", "Fair", "Good", "Strong"];

        const update = () => {
            const value = password.value || "";
            let score = 0;
            if (value.length >= 6) score += 1;
            if (value.length >= 8) score += 1;
            if (/\d/.test(value)) score += 1;
            if (/[^\w\s]/.test(value)) score += 1;

            bars.forEach((bar, idx) => {
                bar.style.background = idx < score ? colors[Math.max(score - 1, 0)] : "#E2E8F0";
            });
            label.textContent = labels[Math.max(score - 1, 0)];
            label.style.color = score > 0 ? colors[Math.max(score - 1, 0)] : "#64748B";
        };

        password.addEventListener("input", update);
        update();
    };

    const initConfirmMatch = () => {
        const password = document.querySelector("#Password.password-input");
        const confirm = document.querySelector("#ConfirmPassword.confirm-password-input");
        const indicator = document.querySelector(".match-indicator");
        if (!password || !confirm || !indicator) return;

        const update = () => {
            if (!confirm.value) {
                indicator.textContent = "";
                indicator.classList.remove("ok", "bad");
                confirm.classList.remove("error");
                return;
            }
            const matches = confirm.value === password.value;
            indicator.textContent = matches ? "✓" : "✗";
            indicator.classList.toggle("ok", matches);
            indicator.classList.toggle("bad", !matches);
            confirm.classList.toggle("error", !matches);
        };

        confirm.addEventListener("input", update);
        password.addEventListener("input", update);
        update();
    };

    const initSubmitLoading = () => {
        document.querySelectorAll("form button[type='submit'][data-loading-text]").forEach((button) => {
            const form = button.closest("form");
            if (!form) return;
            form.addEventListener("submit", () => {
                button.disabled = true;
                const loadingText = button.getAttribute("data-loading-text") || "Loading...";
                button.innerHTML = `<span class="btn-spinner" aria-hidden="true"></span><span>${loadingText}</span>`;
            });
        });
    };

    const initInputErrorClass = () => {
        const checkValidationState = () => {
            document.querySelectorAll(".field-error").forEach((error) => {
                const isError = Boolean(error.textContent && error.textContent.trim().length);
                const field = error.closest(".field");
                const input = field?.querySelector(".form-input");
                if (!input) return;
                input.classList.toggle("error", isError);
            });
        };

        checkValidationState();
        document.querySelectorAll(".form-input").forEach((input) => {
            input.addEventListener("focus", () => input.classList.remove("error"));
        });
    };

    initPasswordToggles();
    initStrengthMeter();
    initConfirmMatch();
    initSubmitLoading();
    initInputErrorClass();
})();
