## 2025-05-15 - Dynamic Accessibility Labels in WPF
**Learning:** For stateful icons (like Play/Pause), providing a dynamic property (e.g., `PlayPauseActionText`) for `AutomationProperties.Name` and `ToolTip` ensures screen readers and users get context-aware feedback that matches the visual state.
**Action:** Always pair state-changing icons with a corresponding string property in the ViewModel for accessibility.

## 2025-05-15 - Conditional Visibility for "Clear" Buttons
**Learning:** In WPF, using `DataTriggers` in a `Style` to monitor `TextBox.Text` for both `""` (empty string) and `{x:Null}` is a clean way to manage the visibility of a "Clear" button without adding extra properties to the ViewModel.
**Action:** Use `DataTriggers` on the `ElementName` of the `TextBox` for UI-only visibility logic like search clear buttons.
