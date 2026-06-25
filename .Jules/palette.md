## 2025-05-15 - [WPF ContextMenu Command Binding]
**Learning:** ContextMenu resides in a separate visual tree, so standard DataContext inheritance fails. Commands must bind using `PlacementTarget.DataContext` and `RelativeSource`.
**Action:** Use `Command="{Binding PlacementTarget.DataContext.MyCommand, RelativeSource={RelativeSource Self}}"` for ContextMenu items.

## 2025-05-15 - [WPF Style Conflict MC3024]
**Learning:** Assigning a 'Style' attribute directly to an element while also defining a nested 'Style' block with 'BasedOn' results in build error MC3024.
**Action:** Define the base style in the nested 'Style' tag using 'BasedOn' and omit the tag-level 'Style' attribute.
