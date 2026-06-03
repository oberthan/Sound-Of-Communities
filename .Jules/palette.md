## 2025-05-15 - Search Placeholder and Accessibility Improvements

**Learning:** WPF TextBox does not have a native "Placeholder" or "Hint" property. To implement this pattern, an overlaying TextBlock with DataTriggers is required to manage visibility based on the TextBox content. Additionally, using TextBlock for interactive icons (like a Play button) prevents keyboard accessibility; these should always be wrapped in a Button with a customized ControlTemplate.

**Action:** Use the overlay TextBlock pattern for hints and ensure all interactive elements are focusable Buttons with `AutomationProperties.Name` and `ToolTip`.
