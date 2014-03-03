// JavaScript Document
function createTabs(id, homeid)
{
	$(function() {
		var $tabsUI = $(id);
    	$tabsUI.tabs({
			//TODO: Get these effects working
			fxFade: true, 
			fxSpeed: 'slow',
			beforeActivate: function (event, ui) {
				ui.oldTab.attr('class', '');
				ui.newTab.attr('class', 'active');
				var id = $(ui.newTab.prevObject).prop('id');
					if(id != homeid) {
						window.location.hash = id;
					}
					else {
						window.location.hash = '';
					}
			},
			beforeLoad: function( event, ui ) {
				ui.jqXHR.error(function() {
					ui.panel.html("Couldn't load this tab." );
				});
			}
		});
		// Switch to correct tab when URL changes (back/forward browser buttons)
		$(window).bind('hashchange', function() {
			if (location.hash !== '') {
				var id = location.hash.substring(1); //Get rid of #
				var tabNum = $('a[id=' + id + ']').parent().index();
				
				if(tabNum > 0){
					$tabsUI.tabs('option', 'active', tabNum);
				} //else: Not one of the main tabs
			} else {
				$tabsUI.tabs('option', 'active', 0);
			}
		});
	});
}