package platformy.technologiczne.lab4.services;

import org.springframework.stereotype.Service;
import platformy.technologiczne.lab4.models.Movies;

import javax.persistence.EntityManager;
import java.util.List;

@Service
public class MovieService extends EntityService<Movies> {

    public MovieService(EntityManager em){
        super(em, Movies.class, Movies::getId);
    }

    public List<Movies> findAll(){
        return em.createNamedQuery(Movies.findAll, Movies.class).getResultList();
    }

    public Float getTotalPrice(List<Movies> movies){
        if (movies == null) {
            return 0.0f;
        }

        Float total = 0.0f;
        for(Movies movie : movies){
            total +=  em.find(Movies.class, movie.getId()).getPrice();
        }
        return total;
    }

}
