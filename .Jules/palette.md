## 2026-06-28 - [WPF Property Precedence & Dynamic Styles]
**Learning:** In WPF, direct element attributes (e.g., `BorderBrush="Transparent"`) take precedence over Style Trigger setters. For dynamic visual feedback like focus states to work via Triggers, the default property values must be defined within the Style's Setters instead of directly on the element.
**Action:** Always define default visual properties (BorderBrush, Visibility, etc.) inside Style Setters when those properties are intended to be modified by DataTriggers or property triggers.
