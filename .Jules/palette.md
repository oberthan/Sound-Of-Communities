## 2025-05-15 - [Search Bar "Clear" Pattern]
**Learning:** Search inputs should provide a quick way to reset filters. A "✕" button that appears only when text is present is an expected micro-interaction. For accessibility, this button needs an ARIA equivalent (AutomationProperties.Name) and a ToolTip.
**Action:** Implement search clear buttons using a DataTrigger bound to `SearchText.Length` with `FallbackValue=0` to ensure robust visibility management in WPF.
