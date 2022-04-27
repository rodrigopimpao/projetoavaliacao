app.controller('EditDialogInstCtrl', function($scope, $uibModalInstance, selectedItem){
    $scope.item = selectedItem;

    $scope.save = function(){
      $uibModalInstance.close($scope.item);
    };
    
    $scope.cancel = function(){
      $uibModalInstance.dismiss('cancel');
    }
  });