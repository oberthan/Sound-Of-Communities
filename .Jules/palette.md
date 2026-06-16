## 2025-06-16 - [WPF Style Conflict Handling]
**Learning:** In WPF XAML, you cannot set the `Style` attribute on an element if you are also defining a nested `Style` block (e.g., `<Button.Style>`). Doing so causes a build error because the property is set twice.
**Action:** Always use `BasedOn` within the nested `<Element.Style>` block to inherit from a base style, and remove the `Style="..."` attribute from the element tag itself.
