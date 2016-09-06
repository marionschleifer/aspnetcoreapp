function sortNoteItems(sortFunction) {
  // sort function: see https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort#Description
  var noteItems = $('.note-item').get();
  noteItems.sort(sortFunction);
  for (var i = 0; i < noteItems.length; i++) {
    noteItems[i].parentNode.appendChild(noteItems[i]);
  }
}

var importanceReverse = false;
$('.sort-by-importance').click(function() {
  sortNoteItems(function(noteItemElementA, noteItemElementB) {
    var importanceA = $(noteItemElementA).data('importance');
    var importanceB = $(noteItemElementB).data('importance');
    if (importanceReverse) {
      return importanceA - importanceB;
    } else {
      return importanceB - importanceA;
    }
  });
  importanceReverse = !importanceReverse;
});

var dateReverse = false;
$('.sort-by-finish-date').click(function() {
  sortNoteItems(function(noteItemElementA, noteItemElementB) {
    var dateA = $(noteItemElementA).data('date');
    var dateB = $(noteItemElementB).data('date');
    if (dateReverse) {
      return dateA - dateB;
    } else {
      return dateB - dateA;
    }
  });
  dateReverse = !dateReverse;
});

var createdAtReverse = false;
$('.sory-by-create-date').click(function() {
  sortNoteItems(function(noteItemElementA, noteItemElementB) {
    var dateA = $(noteItemElementA).data('date');
    var dateB = $(noteItemElementB).data('date');
    if (createdAtReverse) {
      return dateA - dateB;
    } else {
      return dateB - dateA;
    }
  });
  createdAtReverse = !createdAtReverse;
});