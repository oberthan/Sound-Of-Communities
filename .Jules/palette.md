## 2025-05-15 - [WPF Search Bar UX Polish]
**Learning:** WPF property precedence is a critical pitfall; local element attributes override style triggers. Placeholders need to handle both `null` and `""` values in `MultiDataTrigger`.
**Action:** Always move static properties (like `BorderBrush` or `Foreground`) into Style Setters when using DataTriggers for visual feedback (focus/hover).
