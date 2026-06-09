## 2025-05-15 - [Search Bar Accessibility and Usability]
**Learning:** Using a proper `Button` instead of a `TextBlock` for interactive icons is crucial for keyboard focusability and accessibility in WPF. Additionally, providing a "Clear" button for search inputs significantly improves usability by allowing users to reset filters with a single click.
**Action:** Always implement icon-based actions as `Button` elements with `AutomationProperties.Name` and `ToolTip`. For search inputs, always include a `ClearSearchCommand` and a corresponding UI toggle.

## 2025-05-15 - [XAML Trigger Simplification]
**Learning:** Visibility triggers for a "Clear" button can be simplified by setting a default visibility and using `DataTrigger` for the negative case (empty/null text), avoiding overly complex trigger chains.
**Action:** Use default `Visibility="Visible"` and `DataTrigger` for `Value=""` and `Value="{x:Null}"` to collapse the element.
