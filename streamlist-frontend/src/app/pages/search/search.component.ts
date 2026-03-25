import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { debounceTime, distinctUntilChanged, Subject, switchMap, catchError, of } from 'rxjs';
import { MediaService, MediaResult } from '../../services/media.service';

@Component({
  selector: 'app-search',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './search.component.html',
  styleUrl: './search.component.scss'
})
export class SearchComponent {
  private mediaService = inject(MediaService);

  query = '';
  results: MediaResult[] = [];
  loading = false;
  searched = false;

  private search$ = new Subject<string>();

  readonly posterBase = 'https://image.tmdb.org/t/p/w300';

  constructor() {
    this.search$.pipe(
      debounceTime(400),
      distinctUntilChanged(),
      switchMap(query => {
        if (!query.trim()) return of({ results: [] } as any);
        this.loading = true;
        return this.mediaService.search(query).pipe(
          catchError(() => of({ results: [] }))
        );
      })
    ).subscribe(res => {
      this.results = res.results.filter((r: MediaResult) => r.media_type !== 'person');
      this.loading = false;
      this.searched = true;
    });
  }

  onSearch() {
    this.search$.next(this.query);
  }

  getTitle(item: MediaResult): string {
    return item.title ?? item.name ?? 'Sem título';
  }

  getYear(item: MediaResult): string {
    const date = item.release_date ?? item.first_air_date;
    return date ? date.substring(0, 4) : '—';
  }

  getStars(vote: number): string {
    const stars = Math.round(vote / 2);
    return '★'.repeat(stars) + '☆'.repeat(5 - stars);
  }
}
