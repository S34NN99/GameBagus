using System.Collections.Generic;

public class HashSetStringProperty : CollectionProperty<HashSet<string>, string> {
    public override string GetRuntimeValueAsText() => string.Join(", ", Value);
}