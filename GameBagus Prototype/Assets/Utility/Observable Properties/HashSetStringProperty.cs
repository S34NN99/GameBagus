using System.Collections.Generic;

[System.Obsolete]
public class HashSetStringProperty : CollectionProperty<HashSet<string>, string> {
    public override string GetRuntimeValueAsText() => string.Join(", ", Value);
}