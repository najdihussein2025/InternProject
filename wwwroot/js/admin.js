(() => {
  const body = document.body;
  const sidebarToggle = document.querySelector("[data-sidebar-toggle]");
  const sidebarOverlay = document.querySelector("[data-sidebar-overlay]");
  const closeSidebar = () => body.classList.remove("sidebar-open");
  sidebarToggle?.addEventListener("click", () => body.classList.toggle("sidebar-open"));
  sidebarOverlay?.addEventListener("click", closeSidebar);

  document.querySelectorAll("[data-dropdown]").forEach((dd) => {
    const btn = dd.querySelector("button");
    btn?.addEventListener("click", () => dd.classList.toggle("open"));
  });

  const currentPath = window.location.pathname.toLowerCase();
  document.querySelectorAll("[data-nav-link]").forEach((link) => {
    const href = link.getAttribute("href")?.toLowerCase();
    if (href && currentPath.startsWith(href)) link.classList.add("active");
  });

  document.querySelectorAll("[data-alert-dismiss]").forEach((btn) => {
    btn.addEventListener("click", () => btn.closest("[data-alert]")?.remove());
  });

  const focusable = "a[href], button, input, select, textarea, [tabindex]:not([tabindex='-1'])";
  const openModal = (modal) => {
    if (!modal) return;
    modal.classList.add("open");
    modal.setAttribute("aria-hidden", "false");
    const list = [...modal.querySelectorAll(focusable)];
    list[0]?.focus();
    const trap = (event) => {
      if (event.key === "Escape") closeModal(modal);
      if (event.key !== "Tab" || list.length < 2) return;
      if (event.shiftKey && document.activeElement === list[0]) { event.preventDefault(); list[list.length - 1].focus(); }
      if (!event.shiftKey && document.activeElement === list[list.length - 1]) { event.preventDefault(); list[0].focus(); }
    };
    modal._trap = trap;
    document.addEventListener("keydown", trap);
  };
  const closeModal = (modal) => {
    if (!modal) return;
    modal.classList.remove("open");
    modal.setAttribute("aria-hidden", "true");
    if (modal._trap) document.removeEventListener("keydown", modal._trap);
  };
  document.querySelectorAll("[data-open-modal]").forEach((btn) => btn.addEventListener("click", () => openModal(document.getElementById(btn.dataset.openModal))));
  document.querySelectorAll("[data-close-modal]").forEach((btn) => btn.addEventListener("click", () => closeModal(btn.closest("[data-modal]"))));
  document.querySelectorAll("[data-modal]").forEach((modal) => modal.addEventListener("click", (e) => { if (e.target === modal) closeModal(modal); }));

  document.querySelectorAll("[data-exp-toggle]").forEach((btn) => btn.addEventListener("click", () => btn.parentElement.querySelector("[data-exp-panel]")?.classList.toggle("open")));
  document.querySelectorAll("[data-show-inline]").forEach((btn) => btn.addEventListener("click", () => btn.parentElement.querySelector(`[data-inline-panel='${btn.dataset.showInline}']`)?.classList.toggle("open")));

  document.querySelectorAll("[data-settings-tabs]").forEach((wrap) => {
    const tabs = wrap.querySelectorAll("[data-tab-target]");
    const panels = wrap.querySelectorAll("[data-tab-panel]");
    tabs.forEach((tab) => tab.addEventListener("click", () => {
      tabs.forEach((x) => x.classList.remove("active"));
      panels.forEach((x) => x.classList.remove("active"));
      tab.classList.add("active");
      wrap.querySelector(`[data-tab-panel='${tab.dataset.tabTarget}']`)?.classList.add("active");
    }));
  });

  document.querySelectorAll("[data-filter-tabs='users']").forEach((group) => {
    const rows = document.querySelectorAll("[data-users-table] tbody tr");
    group.querySelectorAll("button").forEach((tab) => tab.addEventListener("click", () => {
      group.querySelectorAll("button").forEach((x) => x.classList.remove("active"));
      tab.classList.add("active");
      const filter = tab.dataset.filter;
      rows.forEach((row) => row.style.display = filter === "all" || row.dataset.status === filter ? "" : "none");
    }));
  });

  document.querySelector("[data-mark-all]")?.addEventListener("click", () => {
    document.querySelectorAll(".notice-item.unread").forEach((el) => el.classList.remove("unread"));
    document.querySelectorAll(".unread-dot").forEach((el) => (el.style.display = "none"));
    const badge = document.querySelector("[data-unread-count]");
    if (badge) badge.textContent = "0";
  });

  const pwd = document.querySelector("[data-password-input]");
  const confirm = document.querySelector("[data-password-confirm]");
  const meter = document.querySelector("[data-password-meter]");
  const match = document.querySelector("[data-password-match]");
  const updateStrength = () => {
    if (!pwd || !meter) return;
    const val = pwd.value;
    let score = 0;
    if (val.length >= 8) score++;
    if (/[A-Z]/.test(val)) score++;
    if (/[0-9]/.test(val)) score++;
    if (/[^A-Za-z0-9]/.test(val)) score++;
    const width = `${score * 25}%`;
    meter.style.width = width;
    meter.style.background = score <= 1 ? "var(--danger)" : score <= 2 ? "var(--warning)" : "var(--green)";
    if (confirm && match) {
      match.textContent = confirm.value.length ? (confirm.value === val ? "Passwords match" : "Passwords do not match") : "";
      match.style.color = confirm.value === val ? "var(--green)" : "var(--danger)";
    }
  };
  pwd?.addEventListener("input", updateStrength);
  confirm?.addEventListener("input", updateStrength);

  document.querySelectorAll("[data-confirm]").forEach((el) => {
    const onClick = (event) => {
      if (!window.confirm(el.getAttribute("data-confirm") || "Are you sure?")) {
        event.preventDefault();
      }
    };
    if (el.tagName === "A") el.addEventListener("click", onClick);
    if (el.tagName === "BUTTON") el.addEventListener("click", onClick);
  });

  const fillEditModal = (trigger) => {
    const modalId = trigger.dataset.openModal;
    const modal = modalId ? document.getElementById(modalId) : null;
    if (!modal) return;
    const fullName = trigger.dataset.fullName || "Sara Ahmed";
    const email = trigger.dataset.email || "sara@internhub.io";
    const major = trigger.dataset.major || "Web Dev";
    const status = trigger.dataset.status || "Active";
    const progress = trigger.dataset.progress || "78";
    const skills = trigger.dataset.skills || ".NET, API, Docker, SQL";

    const nameInput = modal.querySelector("[data-edit-field='fullName']");
    const emailInput = modal.querySelector("[data-edit-field='email']");
    const majorInput = modal.querySelector("[data-edit-field='major']");
    const statusInput = modal.querySelector("[data-edit-field='status']");
    const progressInput = modal.querySelector("[data-edit-field='progress']");
    const skillsInput = modal.querySelector("[data-edit-field='skills']");
    const progressOutput = modal.querySelector("[data-progress-output]");

    if (nameInput) nameInput.value = fullName;
    if (emailInput) emailInput.value = email;
    if (majorInput) majorInput.value = major;
    if (statusInput) statusInput.value = status;
    if (progressInput) progressInput.value = progress;
    if (skillsInput) skillsInput.value = skills;
    if (progressOutput) progressOutput.textContent = `${progress}%`;
  };

  document.querySelectorAll("[data-edit-trigger]").forEach((trigger) => {
    trigger.addEventListener("click", () => {
      fillEditModal(trigger);
    });
  });

  document.querySelectorAll("[data-edit-field='progress']").forEach((range) => {
    range.addEventListener("input", () => {
      const form = range.closest("form");
      const output = form?.querySelector("[data-progress-output]");
      if (output) output.textContent = `${range.value}%`;
    });
  });

  document.querySelectorAll("[data-password-toggle]").forEach((btn) => {
    btn.addEventListener("click", () => {
      const wrapper = btn.closest(".password-input-wrap");
      const input = wrapper?.querySelector("input");
      if (!input) return;
      const show = input.type === "password";
      input.type = show ? "text" : "password";
      btn.textContent = show ? "🙈" : "👁";
      btn.setAttribute("aria-label", show ? "Hide password" : "Show password");
    });
  });

  document.querySelectorAll("[data-ui-only-form]").forEach((form) => {
    form.addEventListener("submit", (event) => event.preventDefault());
  });

  document.querySelectorAll("[data-file-preview-input]").forEach((input) => {
    input.addEventListener("change", () => {
      const targetId = input.dataset.previewTarget;
      const preview = targetId ? document.getElementById(targetId) : null;
      if (!preview) return;
      const selected = input.files && input.files.length ? input.files[0].name : "";
      preview.textContent = selected ? `Selected file: ${selected}` : "";
      preview.classList.toggle("visible", Boolean(selected));
    });
  });
})();
