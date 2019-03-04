package platformy.technologiczne;

import java.io.File;
import java.util.*;

public class DiskDirectory extends DiskElement {
    private Set<DiskElement> children;

    public DiskDirectory(String path) {
        children = new TreeSet<>((o1, o2) -> {
           if(o1.file.length() > o2.file.length()) return 0;
           else return 1;
        });

        this.path = path;
        this.file = new File(path);
        this.name = file.getName();

        this.lastModified = new Date(file.lastModified());
        File[] files = this.file.listFiles();
        for (File it : Objects.requireNonNull(files)) {
            if (it.isDirectory()) {
                DiskDirectory disk = new DiskDirectory(it.getPath());
                this.children.add(Objects.requireNonNull(disk));
            } else {
                DiskFile dfile = new DiskFile(it.getPath());
                this.children.add(Objects.requireNonNull(dfile));
            }
        }
    }

    protected void print(int depth) {

        for (DiskElement it : children) {
            if (it.getClass() == DiskDirectory.class) {
                for (int i = 0; i <= depth; i++) {
                    System.out.print("-");
                }
                DiskDirectory dir = new DiskDirectory(it.getPath());
                System.out.print(dir.name + " K " + format.format(dir.lastModified) + " " + file.length());
                System.out.println();
                dir.print(depth+1);
            }
            else{
                DiskFile dfile = new DiskFile(it.getPath());
                dfile.print(depth+1);
            }
        }

        /*...pozostałe pola i metody...*/
    }

    @Override
    public int compareTo(DiskElement o) {
        return 1;
    }
}
