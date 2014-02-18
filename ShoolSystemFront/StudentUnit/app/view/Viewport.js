Ext.define('StudentUnit.view.Viewport',
{
	extend: 'Ext.container.Viewport',
	alias : 'widget.Viewport',
	layout: 'border',
	frame :  true,
	defaults : {
		split: true,
	},
	items:[{
		xtype: HeadGroupButton
	}]
});