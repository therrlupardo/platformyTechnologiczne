package platformy.technologiczne.lab4.models;


import lombok.EqualsAndHashCode;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.ManyToOne;
import javax.persistence.Table;
import java.util.UUID;

@Entity
@Table(name = "orderedmovies")
@EqualsAndHashCode(of = "id")
public class OrderedMovies {

    @Id
    private UUID id = UUID.randomUUID();

    @Getter
    @Setter
    @ManyToOne
    private Movies movie;

    @Getter
    @Setter
    private Integer amount;

}
