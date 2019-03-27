package platformy.technologiczne.lab4.controllers;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.util.UriComponentsBuilder;
import platformy.technologiczne.lab4.models.Movies;
import platformy.technologiczne.lab4.services.MovieService;

import java.net.URI;
import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/movies")
public class MoviesController {

    private final MovieService movieService;

    public MoviesController(MovieService movieService){
        this.movieService = movieService;
    }

    /**
     *
     * @return list of movies in database
     */
    @GetMapping
    public List<Movies> listMovies(){
        return movieService.findAll();
    }

    /**
     *
     * @param movie Movie class object
     * @param uriComponentsBuilder help with building new utl for movie
     * @return 201 (created) if everything is ok
     *         otherwise 409 (conflict)
     */
    @PostMapping
    public ResponseEntity<Void> addMovie(@RequestBody Movies movie,
                                         UriComponentsBuilder uriComponentsBuilder){
        if(movieService.find(movie.getId()) == null){
            movieService.save(movie);

            URI location = uriComponentsBuilder.path("/movies/{id}").buildAndExpand(movie.getId()).toUri();
            return ResponseEntity.created(location).build();
        }
        else {
            return ResponseEntity.status(HttpStatus.CONFLICT).build();
        }
    }

    /**
     *
     * @param id id of movie to return
     * @return if found return movie with given id
     *         otherwise 404 not found
     */
    @GetMapping("/{id}")
    public ResponseEntity<Movies> getMovie(@PathVariable UUID id){
        Movies movie = movieService.find(id);

        if(movie == null){
            return ResponseEntity.notFound().build();
        }
        else{
            return ResponseEntity.ok(movie);
        }
    }

    /**
     *
     * @param movie Movie class object, to replace old one with same id
     * @return 200 if everything is ok
     *         404 if movie to be replaced was not found.
     */
    @PutMapping("/{id}")
    public ResponseEntity<Void> updateMovie(@RequestBody Movies movie, @PathVariable UUID id){
        if(movieService.find(id) != null){
            movie.setId(id);
            movieService.save(movie);
            return ResponseEntity.ok().build();
        }
        else{
            return ResponseEntity.notFound().build();
        }
    }

}
