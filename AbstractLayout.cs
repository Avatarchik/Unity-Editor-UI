﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSG.Utils;

namespace UnityEditorUI
{
    /// <summary>
    /// Layouts are widgets that can contain other child widgets. All layouts should inherit from AbstractLayout.
    /// </summary>
    internal abstract class AbstractLayout : AbstractWidget, ILayout
    {
        protected bool enabled = true;

        private PropertyBinding<bool, ILayout> enabledProperty;

        /// <summary>
        /// Whether or not to draw this layout and its sub-widgets (default is true).
        /// </summary>
        public IPropertyBinding<bool, ILayout> Enabled { get { return enabledProperty; } }

        private List<AbstractWidget> children = new List<AbstractWidget>();

        protected AbstractLayout(ILayout parent) : 
            base(parent)
        {
            enabledProperty = new PropertyBinding<bool, ILayout>(
                this,
                value => this.enabled = value
            );
        }

        public override void OnGUI()
        {
            children.Each(child => child.OnGUI());
        }

        public override void BindViewModel(object viewModel)
        {
            enabledProperty.BindViewModel(viewModel);

            children.Each(child => child.BindViewModel(viewModel));
        }

        /// <summary>
        /// Creates a new button and adds it to the layout.
        /// </summary>
        public IButton Button()
        {
            var newButton = new Button(this);
            children.Add(newButton);
            return newButton;
        }

        /// <summary>
        /// Creates a new label and adds it to the view.
        /// </summary>
        public ILabel Label()
        {
            var newLabel = new Label(this);
            children.Add(newLabel);
            return newLabel;
        }

        /// <summary>
        /// Creates a new TextBox and adds it to the layout.
        /// </summary>
        public ITextBox TextBox()
        {
            var newTextBox = new TextBox(this);
            children.Add(newTextBox);
            return newTextBox;
        }

        /// <summary>
        /// Widget for choosing dates, similar do TextBox except with date validation built-in.
        /// </summary>
        public IDateTimePicker DateTimePicker()
        {
            var newDateTimePicker = new DateTimePicker(this);
            children.Add(newDateTimePicker);
            return newDateTimePicker;
        }

        /// <summary>
        /// Creates a new drop-down selection box and adds it to the layout.
        /// </summary>
        public IDropdownBox DropdownBox()
        {
            var newDropdownBox = new DropdownBox(this);
            children.Add(newDropdownBox);
            return newDropdownBox;
        }

        /// <summary>
        /// Creates a new checkbox and adds it to the layout.
        /// </summary>
        public ICheckbox Checkbox()
        {
            var newCheckbox = new Checkbox(this);
            children.Add(newCheckbox);
            return newCheckbox;
        }

        /// <summary>
        /// Creates a Vector3 field with X, Y and Z entry boxes.
        /// </summary>
        public IVector3Field Vector3Field()
        {
            var newVector3Field = new Vector3Field(this);
            children.Add(newVector3Field);
            return newVector3Field;
        }

        /// <summary>
        /// Creates a widget for editing layer masks.
        /// </summary>
        public ILayerPicker LayerPicker()
        {
            var newLayerPicker = new LayerPicker(this);
            children.Add(newLayerPicker);
            return newLayerPicker;
        }

        /// <summary>
        /// Inserts a space between other widgets.
        /// </summary>
        public ILayout Spacer()
        {
            var newSpacer = new Spacer(this);
            children.Add(newSpacer);
            return this;
        }

        /// <summary>
        /// Creates a VerticalLayout and adds it to this layout.
        /// </summary>
        public ILayout VerticalLayout()
        {
            var newLayout = new VerticalLayout(this);
            children.Add(newLayout);
            return newLayout;
        }

        /// <summary>
        /// Creates a horizontal layout and adds it to this layout.
        /// </summary>
        public ILayout HorizontalLayout()
        {
            var newLayout = new HorizontalLayout(this);
            children.Add(newLayout);
            return newLayout;
        }
    }
}