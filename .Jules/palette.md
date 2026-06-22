## 2025-05-15 - [WPF Accessibility and Interactivity Patterns]
**Learning:** Icon-only buttons must be semantic 'Button' elements (not TextBlocks) for keyboard focusability. Focus indicators should use 'Transparent' borders by default to prevent layout shifts when the 'AccentBrush' is applied on focus.
**Action:** Always use 'ListIconButton' style for list actions and ensure 'AutomationProperties.Name' and 'ToolTip' are present.

## 2025-05-15 - [Placeholder and Clear Button UX]
**Learning:** In WPF, 'MultiDataTrigger' is effective for managing placeholder visibility (only show when Text is empty AND not focused). 'DataTrigger' on the bound 'Text' property (checking for null and empty) is a clean way to manage "Clear" button visibility without extra converters.
**Action:** Implement search inputs using this 3-column layout (Icon, TextBox, Clear Button) with a focus-responsive container border.
