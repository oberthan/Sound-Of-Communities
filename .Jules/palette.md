## 2025-05-15 - WPF Icon Accessibility
**Learning:** Using `TextBlock` for interactive icons (like play buttons) in WPF lists is not keyboard-accessible. Screen readers also miss them if they lack `AutomationProperties`.
**Action:** Always use a `Button` with a transparent or minimalist template for icons. Use `AutomationProperties.Name` and `ToolTip` to provide context to both screen reader and mouse users.

## 2025-05-15 - Context-Aware Action Labels
**Learning:** For toggle buttons (Play/Pause), the accessibility label should describe the *action* (e.g., "Play") rather than the current *state* (e.g., "Stopped").
**Action:** Implement a ViewModel property (e.g., `PlayPauseActionText`) that toggles based on state to provide dynamic `AutomationProperties.Name` and `ToolTip` values.
