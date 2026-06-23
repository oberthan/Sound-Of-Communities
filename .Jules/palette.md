## 2026-06-23 - [WPF Micro-UX and Accessibility Patterns]
**Learning:** In WPF, interactive icons in lists should be proper Button elements rather than TextBlocks with click events to ensure keyboard focusability. Focus indicators for container borders can be implemented using DataTriggers bound to the internal TextBox's IsFocused property. To avoid layout shifts during focus state transitions, use a default transparent border with the same thickness as the focus border.
**Action:** Always use Buttons for interactive list actions. Implement focus indicators on containers by monitoring child focus states.

## 2026-06-23 - [Persistent Placeholder Context]
**Learning:** In search inputs, keeping the placeholder visible even when focused (until the first character is entered) provides better persistent context for the user than hiding it immediately on focus.
**Action:** Use a simple DataTrigger on Text.Length rather than a MultiDataTrigger involving IsFocused for search placeholders.
