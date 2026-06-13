## 2025-05-14 - [Search Bar Clear Button Pattern]
**Learning:** In WPF, a "Clear" button for a search input can be efficiently implemented using a 3-column Grid (Icon, TextBox, Clear Button) and `DataTriggers` bound to the `TextBox.Text` property. This avoids the need for a separate `BooleanToVisibilityConverter` and keeps the ViewModel clean.
**Action:** Use this pattern for all search inputs: use `DataTrigger` on `Binding Text, ElementName=SearchBox` to set `Visibility` to `Collapsed` when the value is `""` or `{x:Null}`.
