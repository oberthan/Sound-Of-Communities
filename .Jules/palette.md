## 2025-05-15 - [WPF Accessibility & Search UX Patterns]

**Learning:** In WPF, interactive icons in lists (like play buttons) should be implemented as `Button` elements rather than `TextBlock` with click handlers to ensure keyboard focusability and ARIA-equivalent accessibility (`AutomationProperties.Name`). Additionally, for dynamic focus indicators (like a search bar border), property values (like `BorderBrush`) must be defined within Style Setters rather than as direct element attributes to allow `DataTriggers` to take effect due to WPF property precedence.

**Action:** Use the `ListIconButton` style for list actions and always manage state-driven UI (placeholders, focus rings) through XAML `Triggers` to maintain a clean MVVM separation.
