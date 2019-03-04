package platformy.technologiczne;

public class Main {

    public static void main(String[] args) {
        String path = "C:/users/thehe/desktop/studia_repo/";
        DiskDirectory dir = new DiskDirectory(path);
        dir.print(0);
    }
}
