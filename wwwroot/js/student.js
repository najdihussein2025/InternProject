(() => {
  const body = document.body;
  const toggleBtn = document.querySelector("[data-sidebar-toggle]");
  const overlay = document.querySelector("[data-sidebar-overlay]");

  toggleBtn?.addEventListener("click", () => {
    body.classList.toggle("student-sidebar-open");
  });
  overlay?.addEventListener("click", () => body.classList.remove("student-sidebar-open"));

  document.querySelectorAll("[data-password-toggle]").forEach((btn) => {
    btn.addEventListener("click", () => {
      const wrap = btn.closest(".student-pwd-wrap");
      const input = wrap?.querySelector("input");
      if (!input) return;
      const isHidden = input.type === "password";
      input.type = isHidden ? "text" : "password";
      btn.textContent = isHidden ? "🙈" : "👁";
      btn.setAttribute("aria-label", isHidden ? "Hide password" : "Show password");
    });
  });

  document.querySelectorAll(".student-panel, .student-card").forEach((el) => {
    el.addEventListener("mouseenter", () => el.style.transform = "translateY(-2px)");
    el.addEventListener("mouseleave", () => el.style.transform = "translateY(0)");
  });

  const appForm = document.querySelector("[data-application-form]");
  appForm?.addEventListener("submit", (event) => {
    const requiredInputs = appForm.querySelectorAll("[required]");
    let isValid = true;
    requiredInputs.forEach((input) => {
      const hasValue = String(input.value || "").trim().length > 0;
      input.classList.toggle("invalid", !hasValue);
      if (!hasValue) isValid = false;
    });
    if (!isValid) {
      event.preventDefault();
      window.alert("Please fill all required fields before submitting.");
      return;
    }
    event.preventDefault();
    window.alert("Application submitted successfully. Waiting for admin approval.");
    appForm.reset();
    requiredInputs.forEach((input) => input.classList.remove("invalid"));
  });
})();
