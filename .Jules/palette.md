## 2025-05-14 - [Search Bar Clear Button Pattern]
**Learning:** Adding a clear button (✕) to search inputs is a high-value micro-UX improvement. In WPF, this can be efficiently implemented using a `DataTrigger` on the `SearchText` property to manage the button's `Visibility`, ensuring it only appears when there's text to clear.
**Action:** Always include a clear button in search inputs. Ensure it has `AutomationProperties.Name` and a `ToolTip` for accessibility, and use `Visibility="Collapsed"` when the input is empty to keep the UI clean.
