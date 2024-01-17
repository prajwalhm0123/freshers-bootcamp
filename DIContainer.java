import java.util.HashMap;
import java.util.Map;

class DIContainer {
    private final Map<Class<?>, Object> container = new HashMap<>();

    public <T> void register(Class<T> type, T instance) {
        container.put(type, instance);
    }

    public <T> T resolve(Class<T> type) {
        return type.cast(container.get(type));
    }
}

interface FuelPump {
    void pumpFuel();
}

interface StartupMotor {
    void start();
}

abstract class Engine {
    private final StartupMotor startupMotor;
    private final FuelPump fuelPump;

    protected Engine(StartupMotor startupMotor, FuelPump fuelPump) {
        this.startupMotor = startupMotor;
        this.fuelPump = fuelPump;
    }

    public abstract void startEngine();

    protected StartupMotor getStartupMotor() {
        return startupMotor;
    }

    protected FuelPump getFuelPump() {
        return fuelPump;
    }
}

class SimpleFuelPump implements FuelPump {
    @Override
    public void pumpFuel() {
        System.out.println("Fuel is being pumped to the engine.");
    }
}

class SimpleStartupMotor implements StartupMotor {
    @Override
    public void start() {
        System.out.println("Engine is starting...");
    }
}

class SimpleEngine extends Engine {
    public SimpleEngine(StartupMotor startupMotor, FuelPump fuelPump) {
        super(startupMotor, fuelPump);
    }

    @Override
    public void startEngine() {
        getStartupMotor().start();
        getFuelPump().pumpFuel();
        System.out.println("Engine has started.");
    }
}

class Car {
    private final Engine engine;
    private final Transmission transmission;

    public Car(Engine engine, Transmission transmission) {
        this.engine = engine;
        this.transmission = transmission;
    }

    public void startCar() {
        System.out.println("Starting the car...");
        engine.startEngine();
        System.out.println("Car is now running.");
    }
}

class Transmission {}

public class Main {
    public static void main(String[] args) {
        DIContainer diContainer = new DIContainer();

        diContainer.register(FuelPump.class, new SimpleFuelPump());
        diContainer.register(StartupMotor.class, new SimpleStartupMotor());

        FuelPump fuelPump = diContainer.resolve(FuelPump.class);
        StartupMotor startupMotor = diContainer.resolve(StartupMotor.class);

        Engine simpleEngine = new SimpleEngine(startupMotor, fuelPump);
      
        Car myCar = new Car(simpleEngine, new Transmission());

        myCar.startCar();
    }
}
