## 2025-05-15 - [WPF Micro-UX and Accessibility Enhancements]
**Learning:** In WPF, interactive icons should be proper `Button` elements with templates rather than `TextBlock` with click events to ensure keyboard focusability. `ContextMenu` bindings require `PlacementTarget` and `RelativeSource` to correctly reach the parent ViewModel.
**Action:** Always use `Button` for actions and `PlacementTarget` for ContextMenu commands.
