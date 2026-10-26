using UnityEngine;
using DebugUI;

public class DebugUIBuilerExample : DebugUIBuilderBase
{
    [SerializeField] float field;

    protected override void Configure(IDebugUIBuilder builder)
    {
        builder.AddLabel("Label");
        builder.AddButton("Button", () => Debug.Log("Hello!"));
        builder.AddField("Field", () => field, x => field = x);
    }
}