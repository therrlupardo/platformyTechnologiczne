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

}
