package platformy.technologiczne;

import java.io.File;
import java.util.Date;

public class DiskFile extends DiskElement {

    public DiskFile(String path){
        this.file = new File(path);
        this.path = path;
        this.name = file.getName();
        this.lastModified = new Date(file.lastModified());
    }

    protected void print(int depth){
        for(int i = 0; i < depth; i++) {
            System.out.print("-");
        }
        System.out.print(this.name + " P " + format.format(this.lastModified) + " " +  file.length());
        System.out.println();
    }

    @Override
    public int compareTo(DiskElement o) {
        return 1;
    }
}
