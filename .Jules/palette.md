## 2025-05-14 - [Semantic UI Elements for Accessibility]
**Learning:** Using `TextBlock` with `MouseLeftButtonDown` for interactive elements (like 'Play' buttons) prevents keyboard navigation and screen reader identification.
**Action:** Always use a `Button` element with a custom `ControlTemplate` for icon-only interactive elements to ensure they are focusable and correctly identified as buttons by assistive technologies.
>>>>>>> REPLACE
<<<<<<< SEARCH
=======
## 2025-05-14 - [WPF Placeholder Text Pattern]
**Learning:** WPF `TextBox` does not have a native placeholder property.
**Action:** Implement placeholders using an overlay `TextBlock` with `DataTriggers` that monitor both `Text.Length` and `IsFocused` to provide a standard "vanish-on-focus" or "vanish-on-text" behavior.
>>>>>>> REPLACE
