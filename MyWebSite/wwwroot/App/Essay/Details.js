app.controller('EssayDetailsController', ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {

    $scope.isAuthenticated = IsAuthenticated();
    //获取内容，并调用保存接口
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Details/' + $stateParams.id,
        success: function (data) {
            $scope.essay = data;
            $scope.$apply();
            //渲染内容区域
            $('#essayContent').append($scope.essay.content);
            $('pre code').each(function (i, block) {
                hljs.highlightBlock(block);
            });

            //初始化评论
            var gitalk = new Gitalk({
                id: '/EssayDetails/' + $stateParams.id,
                clientID: 'b3311f7c6659a89a1b2a',
                clientSecret: '9576ec7765d9a5a7b3eb51bb8b51c401dafa8fc8',
                repo: 'MySiteComment',
                owner: 'lizzie2008',
                admin: ['lizzie2008'],
            })
            gitalk.render('gitalk-container')
        }
    });
}]);