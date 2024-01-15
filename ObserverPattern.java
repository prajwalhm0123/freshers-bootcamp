import java.util.ArrayList;
import java.util.List;

interface IObserver {
    void update(String message);
}

class Dashboard implements IObserver {
    private String message;

    @Override
    public void update(String message) {
        this.message = message;
        System.out.println("Thread state changed to " + message);
    }
}

enum ThreadState {
    CREATED, RUNNING, ABORTED, SLEEPING, SUSPENDED
}

class CustomThread {
    private String id;
    private ThreadState state;
    private String priority;
    private String culture;
    private List<IObserver> observers;

    public CustomThread(String id, String priority, String culture) {
        this.id = id;
        this.state = ThreadState.CREATED;
        this.priority = priority;
        this.culture = culture;
        observers = new ArrayList<>();
    }

    public ThreadState getState() {
        return state;
    }

    private void notifyObservers() {
        for (IObserver observer : observers) {
            observer.update(state.toString());
        }
    }

    public void start() {
        state = ThreadState.RUNNING;
        notifyObservers();
    }

    public void abort() {
        state = ThreadState.ABORTED;
        notifyObservers();
    }

    public void sleep() {
        state = ThreadState.SLEEPING;
        notifyObservers();
    }

    public void suspend() {
        state = ThreadState.SUSPENDED;
        notifyObservers();
    }

    public void subscribe(IObserver observer) {
        observers.add(observer);
    }

    public void unsubscribe(IObserver observer) {
        observers.remove(observer);
    }
}

public class Main {
    public static void main(String args[]) {
        CustomThread customThread = new CustomThread("1A", "low", "en-US");
        IObserver dashboard = new Dashboard();
        customThread.subscribe(dashboard);

        customThread.start();
        customThread.abort();
        customThread.sleep();
        customThread.suspend();

        customThread.unsubscribe(dashboard);
    }
}
