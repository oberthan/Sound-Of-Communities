## 2025-05-15 - WPF Property Precedence and Style Triggers
**Learning:** In WPF, local property values (set directly on the element tag) have higher precedence than values set via Style Setters or Triggers. If a property like `BorderBrush` is set directly on an element, a Style Trigger targeting that property will not take effect.
**Action:** Always define default values for properties that will be modified by triggers within the `Style.Setters` block, rather than as direct attributes on the element.
