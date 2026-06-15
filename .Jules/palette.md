# Palette's UX Journal

## 2025-05-15 - [WPF Interactive Icons]
**Learning:** Using `TextBlock` with `MouseLeftButtonDown` for interactive icons (like Play buttons) is an anti-pattern in WPF. It prevents keyboard focus and lacks automation properties for screen readers.
**Action:** Always use a `Button` with a custom `ControlTemplate` for interactive icons to ensure keyboard accessibility and proper ARIA-equivalent (AutomationProperties) support.
