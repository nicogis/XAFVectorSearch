﻿using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Blazor.Components.Models;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using Microsoft.AspNetCore.Components;
using XAFVectorSearch.Blazor.Server.Controls.RagChat;



namespace XAFVectorSearch.Blazor.Server.Editors;


[PropertyEditor(typeof(IRagData), "CustomStringEdit", true)]
public class IRagDataPropertyEditor(Type objectType, IModelMemberViewItem model) : BlazorPropertyEditorBase(objectType, model), IComplexViewItem
{
    //IObjectSpace _objectSpace;
    //XafApplication _application;

    public void Setup(IObjectSpace objectSpace, XafApplication application)
    {
        //_objectSpace = objectSpace;
        //_application = application;
    }


    public override RagDataComponentModel ComponentModel => (RagDataComponentModel)base.ComponentModel;

    protected override IComponentModel CreateComponentModel()
    {
        var model = new RagDataComponentModel();

        model.ValueChanged = EventCallback.Factory
            .Create<IRagData>(
                this,
                value =>
                {
                    model.Value = value;
                    OnControlValueChanged();
                    WriteValue();
                });
        return model;
    }

    protected override void ReadValueCore()
    {
        base.ReadValueCore();
        ComponentModel.Value = (IRagData)PropertyValue;
    }

    protected override object GetControlValueCore() => ComponentModel.Value;

    protected override void ApplyReadOnly()
    {
        base.ApplyReadOnly();
        ComponentModel?.SetAttribute("readonly", !AllowEdit);
    }
}
