import java.util.ArrayList;
import java.util.List;

class ConsoleDisplayController {
    private List<String> content;

    public void setContent(List<String> msg) {
        this.content = new ArrayList<>(msg);
    }

    public void display() {
        System.out.print(String.join(", ", content));
    }
}

interface SearchStrategy {
    boolean invoke(String item);
}

class StartsWithStrategy implements SearchStrategy {
    private String startString;

    public StartsWithStrategy(String key) {
        this.startString = key;
    }

    public void setStartsWith(String key) {
        this.startString = key;
    }

    public boolean invoke(String item) {
        return item.startsWith(startString);
    }
}

class StringListFilterController {
    private SearchStrategy predicate;

    public StringListFilterController(SearchStrategy searchStrategyObj) {
        this.predicate = searchStrategyObj;
    }

    public List<String> filter(List<String> stringList) {
        List<String> filteredArray = new ArrayList<>();
        for (String word : stringList) {
            if (predicate.invoke(word)) {
                filteredArray.add(word);
            }
        }
        return filteredArray;
    }
}

public class Main {
    public static void main(String args[]) {
        List<String> arr = List.of("abc", "bcd", "acd");

        StartsWithStrategy searchStrategyObj = new StartsWithStrategy("a");

        StringListFilterController stringListFilterControllerObj = new StringListFilterController(searchStrategyObj);
        List<String> filteredArray = stringListFilterControllerObj.filter(arr);

        ConsoleDisplayController consoleDisplayControllerObj = new ConsoleDisplayController();
        consoleDisplayControllerObj.setContent(filteredArray);
        consoleDisplayControllerObj.display();
    }
}
