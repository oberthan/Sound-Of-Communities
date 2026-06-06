## 2025-05-15 - [Accessible Icon Buttons in WPF]
**Learning:** In WPF, using `TextBlock` with `MouseLeftButtonDown` for interactive icons is inaccessible as it lacks keyboard focus and ARIA-equivalent properties. Replacing it with a `Button` and a custom `ControlTemplate` provides full accessibility while maintaining the desired visual style.
**Action:** Always use `Button` for interactive elements, applying `AutomationProperties.Name` and `ToolTip` for accessibility.

## 2025-05-15 - [Destructive Action Confirmation]
**Learning:** Destructive actions like "Remove from Library" should always be guarded by a confirmation dialog to prevent accidental data loss, especially when triggered from context menus or icon buttons.
**Action:** Implement `System.Windows.MessageBox.Show` or a custom dialog for any non-reversible user action.
