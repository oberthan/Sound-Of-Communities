## 2025-05-15 - [Interactive Element Accessibility]
**Learning:** Using `TextBlock` with mouse events for interactive icons prevents keyboard navigation and screen reader support. Proper `Button` elements with styles and `AutomationProperties.Name` are essential for a professional and accessible UI.
**Action:** Always use `Button` for clickable icons, and apply `ControlTemplate` if custom visuals are needed without losing accessibility features.

## 2025-05-15 - [TextBox Placeholders in WPF]
**Learning:** WPF `TextBox` lacks a native placeholder property. A reliable pattern is using a `TextBlock` overlay with `DataTriggers` monitoring `Text.Length` and `IsFocused`.
**Action:** Implement a reusable style or pattern for placeholders to ensure consistent search and input UX.

## 2025-05-15 - [Visual Feedback for Press State]
**Learning:** Users expect immediate visual feedback on interaction. Missing `IsPressed` states make an app feel "dead" or unresponsive.
**Action:** Include `IsPressed` triggers in base button styles (e.g., slight opacity or scale change) to improve perceived performance and delight.
