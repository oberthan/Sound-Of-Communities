# 🎨 Palette's Journal - MeshWave

## 2025-05-14 - [Initial Setup]
**Learning:** Initializing the journal for MeshWave.
**Action:** Start identifying UX improvements.

## 2025-05-14 - [Accessibility: Icon-only Buttons in ListView]
**Learning:** In WPF, using `TextBlock` with `MouseLeftButtonDown` for interactive icons is an accessibility anti-pattern because it lacks keyboard focus and screen reader semantics.
**Action:** Always use a `Button` with a transparent template or flat styling for interactive icons. Ensure `AutomationProperties.Name` and `ToolTip` are set for every icon-only button to satisfy both screen reader and sighted users.
