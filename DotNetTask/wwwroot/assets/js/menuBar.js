/* Menu Bar*/
buildMenu = function () {
    var mm = "";
    if (JSON.parse(sessionStorage.userSes) != null) {
        mm += '<nav class="mt-2"><ul class="sidebar-nav nav-sidebar" id="sidebar-nav"> ';
        $.each(JSON.parse(sessionStorage.userSes).menus, function (key, value) {
            mm += '<li class="nav-item">';
            mm += '<a class="nav-link collapsed sub-btn' + '" href="' + (value.children != null ? "javascript:void(0)" : value.menuLink ?? "javascript:void(0)") + '" ' + (value.children != null ? 'data-bs-target="#nav-' + value.id + '" data-bs-toggle="collapse"' : '') + '>';
            mm += '<i class="' + value.icon + '"></i>';
            mm += '<span>' + value.menuName + '</span>';
            if (value.children != null)
                mm += '<i class="fas fa-angle-left right"></i>';
            mm += '</a>';
            if (value.children != null) {
                mm += '<ul id="nav-' + value.id + '" class="nav nav-treeview collapse" data-bs-parent="#sidebar-nav">';
                $.each(value.children, function (indexC1, c1) {
                    mm += '<li class="nav-item">';
                    mm += '<a class="nav-link ' + (c1.children != null ? "collapsed" : "collapsed") + '" href="' + (c1.children != null ? "javascript:void(0)" : c1.menuLink ?? "javascript:void(0)") + '"' + (c1.children != null ? ' data-bs-target="#c-nav-' + c1.id + '" data-bs-toggle="collapse"' : '') + '>';
                    mm += '<i class="' + c1.icon + '"></i>';
                    mm += '<span>' + c1.menuName + '</span>';
                    if (c1.children != null)
                        mm += '<i class="bi bi-chevron-down ms-auto"></i>';
                    mm += '</a>';
                    if (c1.children != null) {
                        mm += '<ul id="c-nav-' + c1.id + '">';
                        $.each(c1.children, function (indexC2, c2) {
                            mm += '<li class="nav-item">';
                            mm += '<a class="nav-link ' + (c2.children != null ? "collapsed" : "collapsed") + '" href="' + (c2.children != null ? "javascript:void(0)" : c2.menuLink ?? "javascript:void(0)") + '">';
                            mm += '<i class="' + c2.icon + '"></i>';
                            mm += '<span>' + c2.menuName + '</span>';
                            mm += '</a>';


                            mm += '</li>';
                        });
                        mm += '</ul>';
                    }

                    mm += '</li>';
                });
                mm += '</ul>';
            }
            mm += '</li>';
        });
        mm += '</ul>';
    }
    $('.sidebar').html(mm);
}
buildMenu();