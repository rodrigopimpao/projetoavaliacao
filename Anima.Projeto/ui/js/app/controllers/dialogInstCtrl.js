app.controller('DialogInstCtrl', function($scope, $uibModalInstance, selectedItem) {
    $scope.item = selectedItem;
    
    $scope.submitItem = function() {
      $uibModalInstance.close($scope.item);
    };
    
    $scope.cancel = function() {
      $uibModalInstance.dismiss('cancel');
    };
  });