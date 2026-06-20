## 2025-05-22 - Accessible Icon Buttons in WPF
**Learning:** Using `TextBlock` with mouse events for icons (like Play/Pause) is inaccessible for keyboard users and screen readers.
**Action:** Always use a semantic `Button` with a customized `ControlTemplate` (e.g., `ListIconButton`) and provide `AutomationProperties.Name` and `ToolTip`.

## 2025-05-22 - Search Bar Placeholder and Clear Button
**Learning:** WPF `TextBox` lacks a native placeholder property. A robust UX pattern involves an overlay `TextBlock` and a `Clear` button, both managed via `DataTriggers` or `MultiDataTriggers` on the `Text` and `IsFocused` properties.
**Action:** Replicate the search bar container pattern (Border > Grid > TextBox + TextBlock + Button) to ensure consistent and accessible search functionality.
