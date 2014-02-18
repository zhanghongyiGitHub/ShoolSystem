/**
 * @ date 2014-1-25
 * @ author He
 */


Ext.application({
    requires: ['Ext.container.Viewport'],
    name: 'StudentUnit',

    appFolder: 'app',
	
	autoCreateViewport: true,
	views: [
        'Header.HeadGroupButton'
    ],
	models     : ['treeModel','unitTreeModel'],
	controllers: ['dorController']
});