import java.util.AbstractMap;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

abstract class DocumentPart {
    private String name;
    private Map.Entry<Integer, Integer> position;

    public abstract void paint();
    public abstract void save();
}

class WordDocument {
    private List<DocumentPart> partList = new ArrayList<>();

    public void open() {
        for (DocumentPart docPart : partList) {
            docPart.paint();
        }
    }

    public void save() {
        for (DocumentPart docPart : partList) {
            docPart.save();
        }
    }
}

class Header extends DocumentPart {
    private String title;

    @Override
    public void paint() {
        // display element
    }

    @Override
    public void save() {
        // save element
    }
}

class Footer extends DocumentPart {
    private String text;

    @Override
    public void paint() {
        // display element
    }

    @Override
    public void save() {
        // save element
    }
}

class Paragraph extends DocumentPart {
    private String content;
    private int lines;

    @Override
    public void paint() {
        // display element
    }

    @Override
    public void save() {
        // save element
    }
}

class Hyperlink extends DocumentPart {
    private String link;

    @Override
    public void paint() {
        // display element
    }

    @Override
    public void save() {
        // save element
    }
}

public class Main {
    public static void main(String[] args) {
        System.out.println("code start here");
    }
}
