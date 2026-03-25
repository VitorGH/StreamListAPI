import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface MediaResult {
  id: number;
  title?: string;           // filmes
  name?: string;            // séries
  poster_path: string | null;
  overview: string;
  media_type: 'movie' | 'tv' | 'person';
  release_date?: string;
  first_air_date?: string;
  vote_average: number;
}

export interface TmdbResponse {
  results: MediaResult[];
  total_results: number;
}

@Injectable({ providedIn: 'root' })
export class MediaService {
  private http = inject(HttpClient);

  search(query: string): Observable<TmdbResponse> {
    return this.http.get<TmdbResponse>(
      `${environment.apiUrl}/media/search?query=${encodeURIComponent(query)}`
    );
  }
}
