package platformy.technologiczne.lab4.models;


import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.*;
import java.util.UUID;

@Entity
@Table(name = "movies")
@EqualsAndHashCode(of = "id")
@NamedQueries(value = {
        @NamedQuery(name = Movies.findAll, query = "SELECT m FROM Movies m")
})
public class Movies {
    public static final String findAll = "Movies.RETURN_ALL";

    @Getter
    @Id
    private UUID id = UUID.randomUUID();

    @Getter
    @Setter
    private String title;

    @Getter
    @Setter
    private String director;

    @Getter
    @Setter
    private Integer length;

    @Getter
    @Setter
    private Integer amount;

    @Getter
    @Setter
    private Float price;



}
