package platformy.technologiczne;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.Date;

public abstract class DiskElement implements Comparable<DiskElement>{

    protected File file; //java.io.File;

    protected String path;
    protected String name;
    protected Date lastModified;
    protected SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
    protected abstract void print(int depth);
    public void print() { print(0); }

    public String getPath(){
        return path;
    }
    /*...pozostałe pola i metody...*/
}
